namespace EventBus.Dtos
{
    public record CustomerEBDto(Guid id,string Name): IntegrationEvent;
    
}
