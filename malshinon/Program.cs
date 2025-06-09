using malshinon.dal;

namespace malshinon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PeopleDal P = new PeopleDal();
            P.PersonIdentification("mus");

        }
    }
}
