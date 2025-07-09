namespace API.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() 
            : base("La requete est mal formée ou contient des données invalides")
        {
            
        }

        public BadRequestException(string message) 
            : base(message)
        {
        }
    }
}
