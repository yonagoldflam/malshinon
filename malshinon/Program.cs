using malshinon.dal;

namespace malshinon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Initialization Init = new Initialization();
            Init.PDalIns.PersonIdentification("mu");

        }
    }
}
