namespace EstiloMestre.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : EstiloMestreException
{
    public IList<string> ErrorsMessages { get; set; }
    
    public ErrorOnValidationException(IList<string> errorsMessages) : base(string.Empty)
    {
        ErrorsMessages = errorsMessages;
    }
}