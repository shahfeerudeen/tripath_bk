  using MediatR;

public class CreateCombainedExporterCommand : IRequest<string>
{
    public CombainedExporterRequest Request { get; set; }
}
