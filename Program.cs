using System;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Program;
using Graphics;

namespace Program
{


    class Extensions
    {

        public string TakeInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public string InputBox(int cap)
        {
            string message = "";
            int increment = 1;


            do
            {

                char v = Console.ReadKey(true).KeyChar;

                if (v == '\b')
                {
                    if (increment != 1)
                    {
                        increment--;
                        message = message.Substring(0, message.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (v == '\r')
                {
                    break;
                }
                else if (increment < cap+1)
                {
                    increment++;
                    message = message + v;
                    Console.Write(v);
                }


            } while (true);
            Console.Write("\n");

            return message;
        }

    }

    class Keypad
    {
        public string input = "";
        public int char_cap = 47;
        public Dictionary<char, char[]> keycode_chars = new Dictionary<char, char[]>
        {
            // excludes "#", as that toggles the case in decoding
            { '1', new char[] {'1', '.', ',', '?', '!'}},
            { '2', new char[] {'2', 'a', 'b', 'c'}},
            { '3', new char[] { '3', 'd', 'e', 'f' } },
            { '4', new char[] { '4', 'g', 'h', 'i' } },
            { '5', new char[] { '5', 'j', 'k', 'l' } },
            { '6', new char[] { '6', 'm', 'n', 'o' } },
            { '7', new char[] { '7', 'p', 'q', 'r', 's' } },
            { '8', new char[] { '8', 't', 'u', 'v' } },
            { '9', new char[] { '9', 'w', 'x', 'y', 'z' } },
            { '*', new char[] { '*', Convert.ToChar("'"), '-', '+', '=' } },
            { '0', new char[] { '0', ' ' } }
        };
        public KPGraphics KeypadGraphics = new KPGraphics();


        public List<string> SplitStringCommand(string comm)
        {
            // EXAMPLE: 111 0 #2 22 222
            // -> ", ABC"

            List<string> cmds = new List<string>();
            char current_character = ' ';
            int char_occurences = 0;

            foreach (char c in comm.ToCharArray())
            {
                switch (c)
                {

                    // empty space / dashes are the splitting point for each character
                    // if buffer isn't zero, append a string the length of the buffer to a list and reset it
                    case ' ':
                    case '-':
                        if (char_occurences > 0)
                        {
                            cmds.Add(new string(current_character, char_occurences));
                            char_occurences = 0;
                        }
                        break;

                    default:
                        // different char to current -> append new buffer
                        // new characters (that are valid) will increment a buffer
                        if (c != current_character)
                        {
                            // 
                            if (char_occurences > 0)
                            {
                                cmds.Add(new string(current_character, char_occurences));
                            }
                            char_occurences = 1;
                            current_character = c;
                        }
                        else
                        {
                            char_occurences++;
                        }
                        ;
                        break;
                }
                ;
            }
            ;

            if (char_occurences != 0)
            {
                cmds.Add(new string(current_character, char_occurences));
            }
            ;

            return cmds;
        }

        public Tuple<string, bool> HandleKeypadCommands(string comm, ref string output_true)
        {
            
            // program variables
            int character_length = 0;
            char first_char = ' ';
            string resulting_char = ""; // is a string as the character may be capitalized
            bool isCapitalized = false;


            // output
            string output = "";
            bool willEraseTuple = false;


            foreach (string l in SplitStringCommand(comm))
            {
                character_length = l.Length - 1;
                first_char = l[0];
                

                // special functions for specific keys
                switch(first_char)
                {
                    case '#':
                        isCapitalized = (!isCapitalized);
                        continue;
                    case 'r':
                        output_true = "";
                        output = "";
                        continue;
                    case 'd':
                        if (output.Length > 0)
                        {
                            output = output.Substring(0, output.Length - 1);
                        }
                        else if (output_true.Length > 0)
                        {
                            output_true = output_true.Substring(0, output_true.Length - 1);
                        }
                        continue;

                    case 'h':
                        willEraseTuple = true;
                        continue;
                }

                // break out if length of outputs is over cap
                if (output_true.Length + output.Length > char_cap)
                {
                    break;
                }

                // check if key is valid (keypad, r or h)
                if (!keycode_chars.ContainsKey(first_char))
                {
                    continue;
                }

                // cap character length if it exceeds the possible characters provided
                if (character_length > keycode_chars[first_char].Length - 1)
                {
                    character_length = keycode_chars[first_char].Length - 1;
                }

                // take character (and optionally capitalize it)
                resulting_char = Convert.ToString(keycode_chars[first_char][character_length]);
                if (isCapitalized) {
                    resulting_char = resulting_char.ToUpper();
                }
                
                // output
                output += resulting_char;
            }

            return new Tuple<string, bool>(output, willEraseTuple);
        }

        public void RenderKeypad()
        {
            Console.WriteLine(KeypadGraphics.KeypadBox);

            Console.SetCursorPosition(6, Console.CursorTop - 25);
            Console.WriteLine(input);
            Console.SetCursorPosition(6, Console.CursorTop + 25);

                
        }

        public void RenderSideMessage(string header, string[] content)
        {
            // initailize lines and counter
            string[] lines = new string[28];
            int counter = 3;

            lines[0] = header;
            lines[1] = KeypadGraphics.Headers[0];

            // append content to lines
            foreach(string line in content)
            {
                lines[counter] = line;
                counter++;
            }

            // clear previous text
            Console.SetCursorPosition(65, 0);
            foreach (string _ in lines)
            {
                Console.SetCursorPosition(63, Console.CursorTop);
                Console.WriteLine(new string(' ', Console.WindowWidth - 63));
            }

            // write new text
            Console.SetCursorPosition(60, 0);
            foreach (string line in lines)
            {
                Console.SetCursorPosition(60, Console.CursorTop);
                Console.WriteLine(" | " + line);
            }

            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }

        

    }

    class MainEntrypoint
    {
        static void Main(string[] args)
        {
            Console.Title = "keypad-challenge v1 / main";
            Console.SetWindowSize(120, 30);

            
            Keypad newKPD = new Keypad();
            Extensions newExt = new Extensions();
            
            List<string> v = new List<string>();
            Tuple<string, bool> keypad_output = new Tuple<string, bool> ("", false);
            string appending = "";



            
            do
            {
                // state
                Console.Title = "keypad-challenge v1 / main";

                // render keypad and box
                newKPD.RenderKeypad();
                Console.SetCursorPosition(0, Console.CursorTop - 27);
                newKPD.RenderSideMessage(newKPD.KeypadGraphics.Headers[2], newKPD.KeypadGraphics.HeaderText_Standard);
                
                Console.SetCursorPosition(6, Console.CursorTop - 25);

                keypad_output = newKPD.HandleKeypadCommands(newExt.InputBox(47), ref newKPD.input);
                newKPD.input += keypad_output.Item1;
                
                // render help menu
                if ( keypad_output.Item2 == true )
                {
                    Console.Title = "keypad-challenge v1 / help";
                    Console.SetCursorPosition(60, Console.CursorTop - 4);
                    newKPD.RenderSideMessage(newKPD.KeypadGraphics.Headers[1], newKPD.KeypadGraphics.HeaderText_Help);
                    Console.SetCursorPosition(63, Console.CursorTop - 3);
                    Console.Write("return /> ");
                    Console.ReadLine();
                }

                Console.SetCursorPosition(0, 0);

            } while (true);

        }
    }

}