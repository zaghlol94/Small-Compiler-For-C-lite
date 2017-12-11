using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scanner
{
    
    public partial class Form1 : Form
    {
        public int index = 0;
        public Token[] T;
        public List<Var> V = new List<Var>();               // To contain variables
        public List<ArrVar> ArrayV = new List<ArrVar>();
        public List<Token> varTypes = new List<Token>();
        public Token[] temp;
        public string[] reservedWords= {"bool" , "else" , "float" , "int" , "true" , "char" , "false" , "if" , "main" , "while"};
        public string[] reservedSymbols = { "+", ";","-", "*","%", "/",">","<","=","!","|","&", "(", ")", "[", "]", "{", "}" , "||" , "&&" , ">=" , "<=" , "==" , "!=" };
        public string[] tokens;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private string[] TextSplit(string text)
        {
            return text.Split(new char[] { ' ', '\r' , '\n'});
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            TokenList.Items.Clear();
            TypeList.Items.Clear();
            tokens = TextSplit(ScanText.Text);  //Split into string array of tokens
            temp = new Token[tokens.Length];
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == "")
                    continue;
                temp[i] = new Token();
                temp[i].key = tokens[i];

                if (ReservedWordCheck(tokens[i]))
                    temp[i].type = "Reserved Word";
                else if (ReservedSymbolCheck(tokens[i])) //
                    temp[i].type = "Symbol";
                else if (IdentifierCheck(tokens[i]))
                    temp[i].type = "Identifier";
                else if (BoolCheck(tokens[i]))
                    temp[i].type = "Bool";
                else if (IntegerCheck(tokens[i]))
                    temp[i].type = "Integer";
                else if (FloatCheck(tokens[i]))
                    temp[i].type = "Float";
                else if (CharCheck(tokens[i]))
                    temp[i].type = "Char";
                else
                    temp[i].type = "Error !";
            }
            int count = 0;  // count non-Null tokens in temp
            foreach (Token t in temp)
            {
                if (t != null)
                    count++;
            }
            T = new Token[count];
            int j = 0;
            foreach (Token t in temp)
            {
                if(t != null)
                {
                    T[j] = t;
                    j++;
                }
            }
            foreach (Token t in T)
            {
                if (t != null)
                {
                    TokenList.Items.Add(t.key);
                    TypeList.Items.Add(t.type);
                }

            }
        }
        private bool ReservedWordCheck(string token)
        {
            foreach (string s in reservedWords)
            {
                if (token == s)
                    return true;
            }
            return false;
        }
        private bool ReservedSymbolCheck(string token)
        {
            foreach (string c in reservedSymbols)
            {
                if (token == c)
                    return true;
            }
            return false;
        }
        private bool CharCheck(string token)
        {
            int currentState = 0; // start state
            foreach (char c in token)
            {
                switch (currentState)
                {
                    case 0:
                        if (c=='\'')
                            currentState = 1;
                        else
                            return false;
                        break;
                    case 1:
                            currentState = 2;
                        break;
                    case 2:
                        if (c == '\'')
                            currentState = 3;
                        else
                            return false;
                        break;
                    case 3:
                        return false;
                }
            }
            return true;
        }
        private bool FloatCheck(string token)
        {
            int currentState = 0; // start state
            foreach (char c in token)
            {
                switch (currentState)
                {
                    case 0:
                        if (char.IsDigit(c))
                            currentState = 0;
                        else if (c == '.')
                            currentState = 1;
                        else
                            return false;
                        break;
                    case 1:
                        if (char.IsDigit(c))
                            currentState = 2;
                        else
                            return false;
                        break;
                    case 2:
                        if (char.IsDigit(c))
                            currentState = 2;
                        else
                            return false;
                        break;
                }
            }
            return true;
        }
        private bool IntegerCheck(string token)
        {
            int currentState = 0; // start state
            foreach (char c in token)
            {
                switch (currentState)
                {
                    case 0:
                        if (char.IsDigit(c))
                            currentState = 1;
                        else
                            return false;
                        break;
                    case 1:
                        if (char.IsDigit(c))
                            currentState = 1;
                        else
                            return false;
                        break;
                }
            }
            return true;
        }
        private bool BoolCheck(string token)
        {
            if (token == "true" || token == "false")
                return true;
            else
                return false;
        }
        private bool IdentifierCheck(string token)
        {
            int currentState = 0; // start state
            foreach( char c in token)
            {
                switch (currentState)
                {
                    case 0 :
                        if (char.IsLetter(c))
                            currentState = 1;
                        else
                            return false;
                        break;
                    case 1:
                        if (char.IsLetterOrDigit(c))
                            currentState = 1;
                        else
                            return false;
                        break;
                }
            }
            return true;
        }
        private bool matchKey(string s)
        {
            if (index >= T.Length)
            {
                MessageBox.Show("Incomplete Sentence");
                return false;
            }
            if (T[index].key == s)
            {
                index++;
                return true;
            }
            else
            {
                string ErrorText = string.Format("Error : expected '{1}' instead of '{0}' ", T[index].key, s);
                ParseListBox.Items.Add(ErrorText);
                index++;
                return false;
            }
        }
        private bool matchType(string s)
        {
            if (index >= T.Length)
            {
                MessageBox.Show("Incomplete Sentence");
                return false;
            }
            if (T[index].type == s)
            {
                index++;
                return true;
            }
            else
            {
                string ErrorText = string.Format("Error : expected Type '{1}' instead of '{0}' ", T[index].type, s);
                ParseListBox.Items.Add(ErrorText);
                index++;
                return false;
            }
        }
        private void Program()
        {
            matchKey("int");
            matchKey("main");
            matchKey("(");
            matchKey(")");
            matchKey("{");
            Declarations();
            Statements();
            matchKey("}");
        }
        private void Declarations()
        {
            if (CheckIndexRange())
            {
                while ( CheckIndexRange() && (T[index].key == "int" || T[index].key == "char" || T[index].key == "float" || T[index].key == "bool" ))
                {
                    Declaration();
                }
            }
            

        }
        private bool Declaration()
        {
            if (CheckIndexRange())
            {
                if (T[index].key == "int")
                {
                    matchKey("int");
                }
                else if (T[index].key == "char")
                {
                    matchKey("char");
                }
                else if (T[index].key == "float")
                {
                    matchKey("float");
                }
                else if (T[index].key == "bool")
                {
                    matchKey("bool");
                }

                matchType("Identifier");
                if (T[index].key == "[")
                {
                    matchKey("[");
                    matchType("Integer");
                    matchKey("]");
                }
                while (CheckIndexRange() && T[index].key == ",")
                {
                    matchKey(",");
                    matchType("Identifier");
                    if (T[index].key == "[")
                    {
                        matchKey("[");
                        matchType("Integer");
                        matchKey("]");
                    }
                }
                matchKey(";");
                return true;
            }
            return true;
        }
        private void Statements()
        {
            if (CheckIndexRange())
            {
                while (CheckIndexRange() && (T[index].key == ";" || T[index].key == "{" || T[index].key == "if" ||
                T[index].key == "while" || T[index].type == "Identifier"))
                {
                    Statement();
                }
            }
            
        }
        private void Statement()
        {
            if(CheckIndexRange())
            {
                if (T[index].key == ";")
                {
                    matchKey(";");
                }
                else if (T[index].key == "{") //Block
                {
                    matchKey("{");
                    Statements();
                    matchKey("}");
                }
                else if (T[index].key == "if") //IfStatement
                {
                    matchKey("if");
                    matchKey("(");
                    Expression();
                    matchKey(")");
                    Statement();
                    if (T[index].key == "else")
                    {
                        matchKey("else");
                        Statement();
                    }

                }
                else if (T[index].key == "while") // WhileStatement
                {
                    matchKey("while");
                    matchKey("(");
                    Expression();
                    matchKey(")");
                    Statement();
                }
                else if (T[index].type == "Identifier")
                {
                    matchType("Identifier");
                    if (T[index].key == "[")
                    {
                        matchKey("[");
                        Expression();
                        matchKey("]");
                    }
                    matchKey("=");
                    Expression();
                    matchKey(";");
                }
            }
            
        }
        private void Expression()
        {
            Conjunction();
            if (CheckIndexRange())
            {
                while (CheckIndexRange() && T[index].key == "||")
                {
                    matchKey("||");
                    Conjunction();
                }
            }
            
        }
        private void Conjunction()
        {
            Equality();
            if (CheckIndexRange())
            {
                while (CheckIndexRange() && T[index].key == "&&")
                {
                    matchKey("&&");
                    Equality();
                }
            }
           
        }
        private void Equality()
        {
            Relation();
            if (CheckIndexRange())
            {
                if (T[index].key == "==") // EquOp
                {
                    matchKey("==");
                    Relation();
                }
                else if (T[index].key == "!=")
                {
                    matchKey("!=");
                    Relation();
                }
            }
            
        }
        private void Relation()
        {
            Addition();
            if (CheckIndexRange())
            {
                if (T[index].key == "<") // RelOp
                {
                    matchKey("<");
                    Addition();
                }
                else if (T[index].key == "<=")
                {
                    matchKey("<=");
                    Addition();
                }
                else if (T[index].key == ">")
                {
                    matchKey(">");
                    Addition();
                }
                else if (T[index].key == ">=")
                {
                    matchKey(">=");
                    Addition();
                }
            }
            
        }
        private void Addition()
        {
            Term();
            if(CheckIndexRange())
            {
                while (CheckIndexRange() && (T[index].key == "+" || T[index].key == "-"))
                {
                    if (T[index].key == "+") //AddOP
                    {
                        matchKey("+");
                        Term();
                    }
                    else if (T[index].key == "-")
                    {
                        matchKey("-");
                        Term();
                    }
                }
            }
            
        }
        private void Term()
        {
            Factor();
            if(CheckIndexRange())
            {
                while (CheckIndexRange() && (T[index].key == "*" || T[index].key == "/" || T[index].key == "%"))
                {
                    if (T[index].key == "*") //MullOp
                    {
                        matchKey("*");
                        Factor();
                    }
                    else if (T[index].key == "/")
                    {
                        matchKey("/");
                        Factor();
                    }
                    else if (T[index].key == "%")
                    {
                        matchKey("%");
                        Factor();
                    }
                }
            }
            
        }
        private void Factor()
        {
            if(CheckIndexRange())
            {
                if (T[index].key == "-") // UnaryOp
                {
                    matchKey("-");
                }
                else if (T[index].key == "!")
                {
                    matchKey("!");
                }
                Primary();
            }
            
        }
        private void Primary()
        {
            if (CheckIndexRange())
            {
                if (T[index].type == "Identifier")
                {
                    matchType("Identifier");
                    if (T[index].key == "[")
                    {
                        matchKey("[");
                        Expression();
                        matchKey("]");
                    }
                }
                else if (T[index].type == "Integer" || T[index].key == "false" || T[index].key == "true" || T[index].type == "Float" || T[index].type == "Char")
                {
                    if (T[index].type == "Integer")
                        matchType("Integer");
                    else if (T[index].key == "false" )
                        matchKey("false");
                    else if (T[index].key == "true")
                        matchKey("true");
                    else if (T[index].type == "Float")
                        matchType("Float");
                    else if (T[index].type == "Char")
                        matchType("Char");
                }
                else if (T[index].key == "(")
                {
                    matchKey("(");
                    Expression();
                    matchKey(")");
                }
                else if (T[index].key == "int" || T[index].key == "char" || T[index].key == "float" || T[index].key == "bool") // Type
                {
                    if (T[index].key == "int")
                    {
                        matchKey("int");
                    }
                    else if (T[index].key == "char")
                    {
                        matchKey("char");
                    }
                    else if (T[index].key == "float")
                    {
                        matchKey("float");
                    }
                    else if (T[index].key == "bool")
                    {
                        matchKey("bool");
                    }
                    matchKey("(");
                    Expression();
                    matchKey(")");
                }
                else
                    ParseListBox.Items.Add("Error : Missing Identifier");
            }
           
        }
        private void ParseButton_Click(object sender, EventArgs e)
        {
            ParseListBox.Items.Clear();
            index = 0;
            Program();
            if ( !CheckEndOfFile())
            {
                ParseListBox.Items.Add("End of File Expected");
            }

            // T contain tokens
        }
        public bool CheckEndOfFile()
        {
            if (index < T.Length)
            {          
                return false;
            }
            return true;
        }
        public bool CheckIndexRange()   
        {
            if (index >= T.Length)
            {
                return false;
            }
            return true;
        }

        private void SemanticButton_Click(object sender, EventArgs e)
        {
            V.Clear();
            varTypes.Clear();
            ArrayV.Clear();
            for (int i = 2; i < T.Length; i++) // To Get variables and arrayVariables in V List and ArrV
            {
                if (T[i].key == "int" || T[i].key == "float" || T[i].key == "char" || T[i].key == "bool")
                {
                    string nestedType = T[i].key;   // hold the type
                    do
                    {
                        if (T[i + 2].key == "[")
                        {
                            ArrVar newVariable = new ArrVar();
                            Var newArrayVariable = new Var();
                            newArrayVariable.type = nestedType;
                            newArrayVariable.isArray = true;
                            newVariable.type = nestedType;
                            i++;
                            newVariable.name = T[i].key;
                            newArrayVariable.name = T[i].key;
                            newVariable.size = int.Parse(T[i + 2].key);
                            ArrayV.Add(newVariable);
                            V.Add(newArrayVariable);
                            i = i + 4;      // to jump to the ','
                        }
                        else
                        {
                            Var newVariable = new Var();
                            newVariable.type = nestedType;
                            i++;
                            newVariable.name = T[i].key;
                            V.Add(newVariable);
                            i++;
                        }
                        
                    } while (T[i].key == ",");

                }
            }
            for (int i =0; i<V.Count; i++)   // To Check two Variables
            {
                for (int j = i+1; j < V.Count; j++)
                {
                    if (V[i].name == V[j].name)
                    {
                        MessageBox.Show("Semantic Error : Two Variables with the same name");
                        return;
                    }
                }
            }
            for(int i = 2; i < T.Length; i++)   // To check all identifiers are declared
            {
                if (T[i].type == "Identifier")
                {
                    if (!CheckIdentifierDeclared(T[i].key))
                    {
                        MessageBox.Show("Semantic Error : Variable Found not declared");
                        return;
                    }
                }
            }
            
            for (int i = 2; i < T.Length; i++)  // To get types
            {
                string KeyValueTaken = string.Empty;
                varTypes.Clear();
                if (T[i].type == "Identifier")
                {
                    bool isArray = false;
                    int sizeOfArray = 0; // just for default value
                    string IdentifierType = string.Empty;
                    foreach (Var v in V)
                    {
                        if (T[i].key == v.name)
                        {
                            IdentifierType = v.type;
                            isArray = v.isArray;
                            if (isArray)
                            {
                                if (T[i + 1].key != "[")
                                {
                                    MessageBox.Show("Semantic Error : Can't access array without Index");
                                    return;
                                }
                                foreach (ArrVar arrV in ArrayV)
                                {
                                    if (v.name == arrV.name)
                                    {
                                        sizeOfArray = arrV.size;        //Hold array size
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    int l;
                    if (isArray)
                    {
                        l = i + 4;
                        i = i + 4;
                    }
                        
                    else
                        l = i + 1;

                    if (T[l].key == "=")        // will split untill  ' ; ' 
                    {
                        for (l = i + 1; T[l].key != ";"; l++)
                        {
                            if (T[l].key == "=" || T[l].key == "+" || T[l].key == "-" || T[l].key == "*" || T[l].key == "/" || T[l].key == "%"
                                || T[l].key == "||" || T[l].key == "&&" || T[l].key == "<" || T[l].key == ">" || T[l].key == "<=" || T[l].key == ">="
                                || T[l].key == "!=" || T[l].key == "==")
                                continue;
                            varTypes.Add(T[l]);
                            KeyValueTaken = T[i].key;
                        }
                        if (isArray)
                        {
                            int index = int.Parse(T[i - 2].key);
                            if (index >= sizeOfArray)
                            {
                                MessageBox.Show("Semantic Error : Can't access Index Out Of Range");
                                return;
                            }
                        }
                    }
                    //////////////////////////////////////////////
                    i = l;      // may be modified
                    bool ValueTaken = true;
                    foreach (Token t in varTypes)
                    {
                        if (t.type == "Identifier")
                        {
                            foreach (Var v in V)
                            {
                                if (t.key == v.name && v.valueTaken == false)
                                {
                                    MessageBox.Show("Semantic Error : '" + t.key +  "' Variable Value not assigned");
                                    ValueTaken = false;
                                    break;
                                }
                            }
                            if (!ValueTaken)
                                break;
                            string IdentifiertypeRH = string.Empty;
                            foreach (Var v in V)
                            {
                                if (t.key == v.name)
                                {
                                    IdentifiertypeRH = v.type;
                                    break;
                                }
                            }

                            
                            if (!(IdentifierType == IdentifiertypeRH))
                            {
                                MessageBox.Show("Semantic Error : Type checking Error !");
                                return;
                            }
                        }
                        else
                        {
                            if (IdentifierType == "int")
                            {
                                if (!(IdentifierType == "int" && t.type == "Integer"))
                                {
                                    MessageBox.Show("Semantic Error : Type checking Error !");
                                    return;
                                }
                            }
                            if (IdentifierType == "float")
                            {
                                if (!(IdentifierType == "float" && t.type == "Float"))
                                {
                                    MessageBox.Show("Semantic Error : Type checking Error !");
                                    return;
                                }
                            }
                            if (IdentifierType == "bool")
                            {
                                if (!(IdentifierType == "bool" && (t.key == "false" || t.key == "true")))
                                {
                                    MessageBox.Show("Semantic Error : Type checking Error !");
                                    return;
                                }
                            }
                            if (IdentifierType == "char")
                            {
                                if (!(IdentifierType == "char" && t.type == "Char"))
                                {
                                    MessageBox.Show("Semantic Error : Type checking Error !");
                                    return;
                                }
                            }
                        }
                        
                    }

                    foreach (Var v in V)
                    {
                            if (KeyValueTaken == v.name)
                                v.valueTaken = ValueTaken;
                            if (!ValueTaken)
                                return;
                    }
                   
                    
                }

            }

            for (int i=2;i<T.Length;i++)
            {
                if(T[i].key=="/")
                {
                    if (T[i + 1].key == "0"|| T[i + 1].key == "0.0")
                    {
                        MessageBox.Show("Semantic Error : Divisible by zero ");
                        return;
                    }
                }
            }


        }
        public bool CheckIdentifierDeclared(string name)
        {
            foreach (Var v in V)
            {
                if (v.name == name)
                {
                    return true;
                }
            }
            return false;
        }

    }

    public class Token
    {
        public string key;
        public string type;
    }

    public class Var
    {
        public string name;
        public string type;
        public bool valueTaken = false;
        public bool isArray = false;
    }
    public class ArrVar         // for array variables
    {
        public string name;
        public string type;
        public int size ;
    }
}
