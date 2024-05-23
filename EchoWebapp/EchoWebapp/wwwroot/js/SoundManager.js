let audioCtx;
let buffer;
// Stereo
let channels = 2;
var myStream;
let mediaRecorder;

//https://www.youtube.com/watch?v=K6L38xk2rkk
//https://www.geeksforgeeks.org/how-to-record-and-play-audio-in-javascript/

function SetDotNetHelper(dotNetHelper) {
    window.dotNetHelper = dotNetHelper;
}

function test() {
    navigator.mediaDevices.getUserMedia({
        audio: true,
    }).then((stream) => {
        myStream = stream
        console.log(myStream)
        toggleAudio(true)
    }).catch((err) => {
        console.log("unable to connect because " + err)
    })
}

function start() {
    mediaRecorder.start();
}

function stop() {
    mediaRecorder.stop();
}

function listenToSound() {
    var audio = document.createElement("AUDIO");
    console.log(audio) 
    if ("srcObject" in audio) {
        audio.srcObject = myStream;
    }
    else {   // Old version
        audio.src = window.URL
            .createObjectURL(myStream);
    }

    audio.onloadedmetadata = function (ev) {

        // Play the audio in the 2nd audio
        // element what is being recorded
        audio.play();
    };
    //// Start record
    //let start = document.getElementById('btnStart');

    //// Stop record
    //let stop = document.getElementById('btnStop');

    //// 2nd audio tag for play the audio
    let playAudio = document.createElement("AUDIO");
    console.log(playAudio) 
    // This is the main thing to recorded 
    // the audio 'MediaRecorder' API
    mediaRecorder = new MediaRecorder(myStream);
    // Chunk array to store the audio data 
    let dataArray = [];
    // Pass the audio stream 

    // Start event
    //start.addEventListener('click', function (ev) {
    //    mediaRecorder.start();
    //    // console.log(mediaRecorder.state);
    //})

    //// Stop event
    //stop.addEventListener('click', function (ev) {
    //    mediaRecorder.stop();
    //    // console.log(mediaRecorder.state);
    //});

    // If audio data available then push 
    // it to the chunk array
    mediaRecorder.ondataavailable = function (ev) {
        dataArray.push(ev.data);
    }

    // Convert the audio data in to blob 
    // after stopping the recording
    mediaRecorder.onstop = function (ev) {

        // blob of type mp3
        let audioData = new Blob(dataArray,
            { 'type': 'audio/mp3;' });

        // After fill up the chunk 
        // array make it empty
        dataArray = [];

        // Creating audio url with reference 
        // of created blob named 'audioData'
        let audioSrc = window.URL
            .createObjectURL(audioData);

        // Pass the audio url to the 2nd video tag
        playAudio.src = audioSrc;
    }
}

function toggleAudio(b) {
    if (b == "true") {
        myStream.getAudioTracks()[0].enabled = true
    }
    else {
        myStream.getAudioTracks()[0].enabled = false
    }
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