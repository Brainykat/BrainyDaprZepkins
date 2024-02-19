namespace EventBus.Dtos
{
    public record CustomerEBDto(Guid customerId,string Name): IntegrationEvent;
    
}
