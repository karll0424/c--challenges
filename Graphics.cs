namespace Graphics
{
    class KPGraphics
    {

        public string[] Headers =
        {
            "------------------------------------------------------ ",
            "HELP //",
            "CHALLENGES //"

        };

        public string[] HeaderText_Help =
        {
            "To enter characters into the Keypad, enter the",
            "corresponding numbers once or multiple times.",
            "",
            "Physical spaces (or Hyphens, '-') separate different",
            "characters, to write multiple characters at once.",
            "",
            "Additionally, different numbers/symbols adjacent to",
            "eachother will also resut in the same effect.",
            "",
            "There is a limit of 47 characters.",
            "",
            "",
            "EXAMPLES //",
            "",
            "   - Entering '1' writes '1'",
            "   - Entering '1-0-11' writes '1 .'",
            "   - Entering '#2#2' writes 'Aa'"

        };

        public string[] HeaderText_Standard = {
            "This program is based on the challenge:",
            "  'Texting with an old-school mobile phone'",
            "",
            "The challenge is adapted a little to provide a",
            "clean user experience.",
            "",
            "",
            "TRY THESE! //",
            " -#1. Write '123'!",
            " -#2. Write 'abc'!",
            " -#3. Write 'Hi!'",
            " -#4. Write your name!",
            " -#5. Write 'Hello, World!'",
            " -#6. Write a phone number!",
            " -#7. Use the keypad to write a face!",
            " -#8. Write a message of your choice!",
            "",
            "",
            "",
            "Thank you for running this program!",
            " - developed by karll0424",
            " - written in c#"
        };

        public string KeypadBox =
        """"""
           
            ,--------------------------------------------------,
            |                                                  |
            |                                                  |
            |                                                  |
            '__________________________________________________'

            [r] - clear    [d] - delete                   KP-024
            [h] - help
                          _______ _________ _______
                        .'       |         |       '.
                        |    1   |    2    |   3    |
                        |  .,?!  |   abc   |  def   |
                        |________|_________|________|
                        |        |         |        |
                        |    4   |    5    |   6    |
                        |   ghi  |   jkl   |  mno   |
                        |________|_________|________|
                        |        |         |        |
                        |    7   |    8    |   9    |
                        |  pqrs  |   tuv   |  wyxz  |
                        |________|_________|________|
                        |        |         |        |
                        |    *   |    0    |   #    |
                        |  '-+=  |  space  |  case  |
                        '._______|_________|_______.'
            
        """""";

    }
}