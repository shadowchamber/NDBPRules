// Template generated code from Antlr4BuildTasks.Template v 8.17
namespace xppantlr
{
    using Antlr4.Runtime;
    using Antlr4.Runtime.Misc;
    using Antlr4.Runtime.Tree;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Program
    {
        static void Main(string[] args)
        {
            Try(System.IO.File.ReadAllText(@"C:\Users\Administrator\source\repos\xppantlr\test.xpp"));
        }

        static void Try(string input)
        {
            var str = new AntlrInputStream(input);
            System.Console.WriteLine(input);
            var lexer = new XppLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new XppParser(tokens);
            var listener_lexer = new ErrorListener<int>();
            var listener_parser = new ErrorListener<IToken>();
            lexer.AddErrorListener(listener_lexer);
            parser.AddErrorListener(listener_parser);
            var tree = parser.compilation_unit();
            if (listener_lexer.had_error || listener_parser.had_error)
                System.Console.WriteLine("error in parse.");
            else
            {
                System.Console.WriteLine("parse completed.");

                //ShowNode("Root", tree);

                Antlr4.Runtime.Tree.ParseTreeWalker walker = new Antlr4.Runtime.Tree.ParseTreeWalker();

                XppListener listener = new XppListener();
                listener.fullText = input;

                walker.Walk(listener, tree);

                string mockText = "";

                mockText += @$"public class {listener.classData.Name}Mock extends {listener.classData.Name}
{{
    private static MockArgsList mockData;

    public void new()
    {{
        mockData = MockArgsList::checkCreate(mockData);
    }}

    public static MockArgsList getMockData()
    {{
        return mockData;
    }}";

                foreach (var method in listener.classData.Methods)
                {
                    string args = $"";
                    string calls = $"{(method.TypeName != "void" ? "return " : "")}mockData.call(methodStr({listener.classData.Name}Mock, {method.Name}))";

                    foreach (var arg in method.Args)
                    {
                        string argtext = $"{arg.TypeName} {arg.Name}";

                        string call = $"\n            .withArg(identifierStr({arg.Name}), {arg.Name})";


                        if (args != "")
                        {
                            args += ", ";
                        }

                        args += argtext;
                        calls += call;
                    }

                    if (method.TypeName != "void")
                    {
                        calls += "\n               .getResult()";
                    }

                    calls += ";";

                    mockText += @$"

    public {method.TypeName} {method.Name}({args})
    {{
        {calls}
    }}";
                }

                mockText += "\n}";

                Console.WriteLine($"mock: {mockText}");
            }
        }

        static void ShowNode(string path, IParseTree node)
        {
            //Console.WriteLine($"{node.ToStringTree()}");
            Console.WriteLine($"{path}: {node.GetText()}");

            for (int i = 0; i < node.ChildCount; ++i)
            {
                ShowNode(path + " ===== " + node.GetText(), node.GetChild(i));
            }
        }

        static string ReadAllInput(string fn)
        {
            var input = System.IO.File.ReadAllText(fn);
            return input;
        }
    }

    public class XppListener : XppParserBaseListener
    {
        public ClassData classData = new ClassData();

        public string fullText = "";

        public MethodData methodData = new MethodData();
        public MethodArgData argData = new MethodArgData();

        bool expectClassName = false;

        public override void EnterClass_definition([NotNull] XppParser.Class_definitionContext context)
        {
            base.EnterClass_definition(context);

            expectClassName = true;
        }

        public override void EnterIdentifier([NotNull] XppParser.IdentifierContext context)
        {
            base.EnterIdentifier(context);

            if (expectClassName)
            {
                this.OnClassName(context.Start.Text);

                expectClassName = false;
            }

            if (methodName)
            {
                this.OnMethodName(context.Start.Text);

                methodName = false;
            }
        }

        public void OnClassName(string name)
        {
            Console.WriteLine($"Class: {name}");

            classData.Name = name;
        }

        bool methodName = false;
        string lastType = "void";

        public override void EnterMethod_declaration([NotNull] XppParser.Method_declarationContext context)
        {
            base.EnterMethod_declaration(context);

            methodName = true;
        }

        public void OnMethodName(string name)
        {
            Console.WriteLine($"Method: {name}");

            methodData = new MethodData();

            methodData.Name = name;
            methodData.TypeName = lastType;
        }

        public void OnArg(string name)
        {
            Console.WriteLine($"Arg: {name}");

            argData.Name = name;
        }

        public void OnArgType(string name)
        {
            Console.WriteLine($"Arg type: {name}");
        }

        public void OnType(string name)
        {
            Console.WriteLine($"Type: {name}");

            lastType = name;

            Console.WriteLine($"Arg Type: {argData.TypeName} => {name}");
            argData.TypeName = name;
        }

        public override void EnterArg_name([NotNull] XppParser.Arg_nameContext context)
        {
            base.EnterArg_name(context);

            this.OnArg(context.Start.Text);
        }

        public override void EnterArg_type_name([NotNull] XppParser.Arg_type_nameContext context)
        {
            base.EnterArg_type_name(context);

            this.OnArgType(context.Start.Text);
        }

        public override void EnterType_([NotNull] XppParser.Type_Context context)
        {
            base.EnterType_(context);

            this.OnType(context.Start.Text);
        }

        public override void ExitFormal_parameter_list([NotNull] XppParser.Formal_parameter_listContext context)
        {
            base.ExitFormal_parameter_list(context);

            Console.WriteLine($"ExitFormal_parameter_list: {context.Start.Text}");
        }

        public override void EnterArg_declaration([NotNull] XppParser.Arg_declarationContext context)
        {
            base.EnterArg_declaration(context);

            argData = new MethodArgData();
        }

        public override void ExitArg_declaration([NotNull] XppParser.Arg_declarationContext context)
        {
            base.ExitArg_declaration(context);

            methodData.Args.Add(argData);
        }

        public override void EnterFormal_parameter_list([NotNull] XppParser.Formal_parameter_listContext context)
        {
            base.EnterFormal_parameter_list(context);
        }

        public override void ExitMethod_declaration([NotNull] XppParser.Method_declarationContext context)
        {
            base.ExitMethod_declaration(context);

            Console.WriteLine($"ExitMethod_declaration: {context.Start.Text}");

            classData.Methods.Add(methodData);
        }

        public override void ExitMethod_body([NotNull] XppParser.Method_bodyContext context)
        {
            base.ExitMethod_body(context);

            lastType = "void";
        }

        public override void EnterExpression([NotNull] XppParser.ExpressionContext context)
        {
            base.EnterExpression(context);

            string expr = fullText.Substring(context.Start.StartIndex, context.Stop.StopIndex - context.Start.StartIndex + 1);

            Console.WriteLine($"EnterExpression: {context.Start.Text} {expr}");
        }
    }

    public class ClassData
    {
        public string Name { get; set; }
        public List<MethodData> Methods { get; set; } = new List<MethodData>();

    }

    public class MethodData
    {
        public string TypeName { get; set; } = "void";
        public string Name { get; set; }
        public List<MethodArgData> Args { get; set; } = new List<MethodArgData>();
    }

    public class MethodArgData
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Default { get; set; }
    }
}
