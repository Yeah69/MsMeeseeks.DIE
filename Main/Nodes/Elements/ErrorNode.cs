using MsMeeseeks.DIE.Logging;

namespace MsMeeseeks.DIE.Nodes.Elements;

internal interface IErrorNode : IElementNode
{
    string Message { get; }
}

internal sealed partial class ErrorNode : IErrorNode
{
    private readonly ITypeSymbol _currentType;
    private readonly ILocalDiagLogger _localDiagLogger;

    internal ErrorNode(
        string message,
        ITypeSymbol currentType,
        ILocalDiagLogger localDiagLogger)
    {
        _currentType = currentType;
        _localDiagLogger = localDiagLogger;
        Message = message;
    }
    
    public void Build(PassedContext passedContext) =>
        _localDiagLogger.Error(
            ErrorLogData.ResolutionException(Message, _currentType, passedContext.ImplementationStack),
            Location.None);

    public string Message { get; }
    public string TypeFullName => "Errors have no type";
    public string Reference => "Errors have no reference";
}