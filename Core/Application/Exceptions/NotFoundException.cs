namespace Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, string key) : base("Entity " + name + " Key: " + key + "No fue encontrado")
        {
        }
    }
}