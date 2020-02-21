using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMLogo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("The number must be an odd integer between 2 and 10000: ");
            string input = Console.ReadLine();
            int n;
            bool parseCheck = int.TryParse(input, out n);
            while (parseCheck == false || n % 2 == 0 || n < 2 || n > 10000)

            {
                Console.Write("Invalid input entered. Input must be an odd integer between 2 and 10000: ");
                input = Console.ReadLine();
                parseCheck = int.TryParse(input, out n);
            }

            LogoGenerator result = new LogoGenerator();
            result.LetterWidth = n;
            Console.WriteLine(result.Logo);
        }
    }

    class LogoGenerator
    {
        private int letterWidth = 0;

        private StringBuilder logoBuilder = new StringBuilder();

        private string logo = null;

        public LogoGenerator() : this(3)
        {

        }
        public LogoGenerator(int letterWidth)
        {
            this.letterWidth = letterWidth;
            GenerateLogo(letterWidth);
        }

        public string Logo
        {
            get { return logo; }
        }

        public int LetterWidth
        {
            set { letterWidth = value; GenerateLogo(value); }
        }

        private string DashSegmentBuilder(int dashCount)
        {
            string dashSegment = null;
            StringBuilder dashSegmentBuilder = new StringBuilder();
            for (int i = 0; i < dashCount; i++)
            {
                dashSegmentBuilder.Append('-');
            }
            dashSegment = dashSegmentBuilder.ToString();
            return dashSegment;
        }

        public string GetLogo()
        {
            return logo;
        }

        private void LowerHalfLogoGenerator(int endDash, int endStar, int middleDash, int middleStar)
        {
            if (endDash >= 0)
            {
                StringBuilder lineBuilder = new StringBuilder();
                lineBuilder.Append(DashSegmentBuilder(endDash));
                lineBuilder.Append(StarSegmentBuilder(endStar));
                lineBuilder.Append(DashSegmentBuilder(middleDash));
                lineBuilder.Append(StarSegmentBuilder(middleStar));
                lineBuilder.Append(DashSegmentBuilder(middleDash));
                lineBuilder.Append(StarSegmentBuilder(endStar));
                lineBuilder.Append(DashSegmentBuilder(endDash));
                string halfLine = lineBuilder.ToString();
                lineBuilder.Append(halfLine);
                logoBuilder.Append(lineBuilder.ToString());
                if (endDash > 0)
                {
                    logoBuilder.Append("\n");
                }
                LowerHalfLogoGenerator(endDash - 1, endStar, middleDash + 2, middleStar - 2);
            }
            else
            {
                logo = logoBuilder.ToString();
                logoBuilder.Clear();
            }
        }

        private string StarSegmentBuilder(int starCount)
        {
            string starSegment = null;
            StringBuilder starSegmentBuilder = new StringBuilder();
            for (int i = 0; i < starCount; i++)
            {
                starSegmentBuilder.Append('*');
            }
            starSegment = starSegmentBuilder.ToString();
            return starSegment;
        }

        private void UpperHalfLogoGenerator(int endDash, int middleDash, int stars)
        {
            if (middleDash >= 1)
            {
                StringBuilder lineBuilder = new StringBuilder();
                lineBuilder.Append(DashSegmentBuilder(endDash));
                lineBuilder.Append(StarSegmentBuilder(stars));
                lineBuilder.Append(DashSegmentBuilder(middleDash));
                lineBuilder.Append(StarSegmentBuilder(stars));
                lineBuilder.Append(DashSegmentBuilder(endDash));
                string halfLine = lineBuilder.ToString();
                lineBuilder.Append(halfLine);
                logoBuilder.Append(lineBuilder.ToString() + '\n');
                UpperHalfLogoGenerator(endDash - 1, middleDash - 2, stars + 2);
            }
            else
            {
                LowerHalfLogoGenerator(endDash, letterWidth, 1, (letterWidth * 2) - 1);
            }
        }

        private void GenerateLogo(int number)
        {
            UpperHalfLogoGenerator(number, number, number);
        }
    }
}