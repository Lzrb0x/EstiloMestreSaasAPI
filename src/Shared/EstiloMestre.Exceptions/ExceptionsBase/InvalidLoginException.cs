namespace EstiloMestre.Exceptions.ExceptionsBase;

public class InvalidLoginException : EstiloMestreException
{
    public InvalidLoginException() : base(ResourceMessagesExceptions.EMAIL_OR_PASSWORD_INVALID) { }
}
