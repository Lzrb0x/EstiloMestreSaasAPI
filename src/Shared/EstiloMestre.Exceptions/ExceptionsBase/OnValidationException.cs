namespace EstiloMestre.Exceptions.ExceptionsBase;

public class OnValidationException : EstiloMestreException
{
    public IList<string> ErrorsMessages { get; set; }

    public OnValidationException(IList<string> errorsMessages) : base(string.Empty)
    {
        ErrorsMessages = errorsMessages;
    }
}
