using System;
using System.Text;

namespace Model.Utils
{
    public class CarUtils
    {
        public static string GenerateVinNumber()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 17; i++)
            {
                sb.Append(RandomChar());
            }

            return sb.ToString();
        }

        private static char RandomChar()
        {
            Random random = new Random();

            char res;
            bool resGood = false;
            do
            {
                res = (char)random.Next(48, 91);
                resGood = res != ':' && res != ';' && res != '<' && res != '=' && res != '>' && res != '?' && res != '@'
                    ? true
                    : false;

            } while (!resGood);

            return res;
        }
    }
}
