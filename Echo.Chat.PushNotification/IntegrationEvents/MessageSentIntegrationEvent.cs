using EventBus.Abstractions;
using EventBus.Events;

namespace Echo.Chat.PushNotification.IntegrationEvents;

public record MessageSentIntegrationEvent() : IntegrationEvent;
