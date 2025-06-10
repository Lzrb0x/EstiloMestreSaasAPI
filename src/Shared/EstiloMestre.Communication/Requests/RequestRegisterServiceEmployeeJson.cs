namespace EstiloMestre.Communication.Requests;

public class RequestRegisterServiceEmployeeJson
{
    public long BarbershopServiceId { get; set; }

    /*
     Can add more properties in the future, like price override.
    allowing to set different attributes for each employee.

    * - We need refactor the association table to allow this.
    */
}
