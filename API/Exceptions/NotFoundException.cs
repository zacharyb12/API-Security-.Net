namespace API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() 
            : base ("L'élément demandé n'a pas été trouvé.")
        {

        }
        public NotFoundException(string message) 
            : base(message)
        {

        }
    }
}
