namespace Assets.Scripts.Other
{
    public static class Util
    {
        public static string Split(string input)
        {
            string result = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    result += ' ';
                }

                result += input[i];
            }

            return result.Trim();
        }
    }
}
