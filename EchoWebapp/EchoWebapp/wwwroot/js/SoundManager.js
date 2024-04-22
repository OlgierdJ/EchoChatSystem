let audioCtx;
let buffer;
// Stereo
let channels = 2;


function SetDotNetHelper(dotNetHelper) {
    window.dotNetHelper = dotNetHelper;
}


function init() {
    audioCtx = new AudioContext();
}

function play() {
    // Create a source node from the buffer
    var source = audioCtx.createBufferSource();
    source.buffer = buffer;
    // Connect to the final output node (the speakers)
    source.connect(audioCtx.destination);
    // Play immediately
    source.start(0);
}

function playByteArray(byteArray) {

    //this is where the shit hits the fan
    var arrayBuffer = new ArrayBuffer(byteArray.length);
    var bufferView = new Uint8Array(arrayBuffer);
    for (i = 0; i < byteArray.length; i++) {
        bufferView[i] = byteArray[i];
    }

    audioCtx.decodeAudioData(arrayBuffer, function (buf) {
        buffer = buf;
        play();
    });
}
window.joinCall = () => {
    //play specific join sound

    //allow audio and microphone (start mediarecorder)
        mediaRecorder.start();
        console.log(mediaRecorder.state);
        console.log("recorder started");
        record.style.background = "red";
        record.style.color = "black";
  
}
window.leaveCall = () => {
    //play specific leave sound

    //allow audio and microphone (start mediarecorder)
    mediaRecorder.stop();
    console.log(mediaRecorder.state);
    console.log("recorder stopped");
    record.style.background = "";
    record.style.color = "";
}

window.playAudio = (streamRef) => {
    console.log(streamRef)
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        console.log("getUserMedia supported.");
        navigator.mediaDevices
            .getUserMedia(
                // constraints - only audio needed for this app
                {
                    audio: true,
                },
            )

            // Success callback
            .then((stream) =>
            {
                //Look at this shit
                //https://developer.mozilla.org/en-US/docs/Web/API/MediaStream_Recording_API/Using_the_MediaStream_Recording_API
                const mediaRecorder = new MediaRecorder(stream);
                console.log(stream)
                mediaRecorder.ondataavailable = (e) => {
                    //send data to c# and relay it to signalr
                    window.dotNetHelper.invokeMethodAsync('RelayVoiceData', e);
                };
            })

            // Error callback
            .catch((err) => {
                console.error(`The following getUserMedia error occurred: ${err}`);
            });
    } else {
        console.log("getUserMedia not supported on your browser!");
    }



    //try {

    
    ////    console.log(streamRef)
    ////const data = streamRef.arrayBuffer();
    //if (!audioCtx) {
    //    init();
    //    }
    ////maybe not sent arraybuffer here
    ////or remove line 23 but do we still need the length of the array...ye maybe?
    //    playByteArray(streamRef);
    ////    data
    ////.then(buff => 
    ////    audioCtx.decodeAudioData(buff)
    ////).then(decoded => {
    ////    var track = audioCtx.createBufferSource();
    ////    track.buffer = decoded;
    ////    track.connect(audioCtx.destination);
    ////    track.start();
    ////});
   
    //// Create an empty two second stereo buffer at the
    //// sample rate of the AudioContext
    ////const frameCount = audioCtx.sampleRate * 2.0;

    ////const buffer = new AudioBuffer({
    ////    numberOfChannels: channels,
    ////    length: frameCount,
    ////    sampleRate: audioCtx.sampleRate,
    ////});

    ////// Fill the buffer with white noise;
    ////// just random values between -1.0 and 1.0
    ////for (let channel = 0; channel < channels; channel++) {
    ////    // This gives us the actual array that contains the data
    ////    const nowBuffering = buffer.getChannelData(channel);
    ////    for (let i = 0; i < frameCount; i++) {
    ////        // Math.random() is in [0; 1.0]
    ////        // audio needs to be in [-1.0; 1.0]
    ////        nowBuffering[i] = Math.random() * 2 - 1;
    ////    }
    ////}

    ////// Get an AudioBufferSourceNode.
    ////// This is the AudioNode to use when we want to play an AudioBuffer
    ////const source = audioCtx.createBufferSource();
    ////// Set the buffer in the AudioBufferSourceNode
    ////source.buffer = buffer;
    ////// Connect the AudioBufferSourceNode to the
    ////// destination so we can hear the sound
    //////source.connect(audioCtx.destination);
    //////// start the source playing
    //////source.start();

    ////source.onended = () => {
    ////    console.log("White noise finished.");
    ////    };
    //} catch (err)
    //{
    //    console.log(err.message)
    //}
};