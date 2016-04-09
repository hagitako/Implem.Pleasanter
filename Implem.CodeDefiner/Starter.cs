﻿using Implem.DefinitionAccessor;
using Implem.Libraries.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
namespace Implem.CodeDefiner
{
    internal class Starter
    {
        static void Main(string[] args)
        {
            Initializer.Initialize(Assembly.GetEntryAssembly().Location, codeDefiner: true);
            Performances.Record(MethodBase.GetCurrentMethod().Name);
            Init();
            ValidateArgs(args);
            var action = args[0];
            var target = args.Length >= 2 ? args[1] : null;
            switch (action)
            {
                case "_rds":
                    ConfigureDatabase();
                    break;
                case "rds":
                    ConfigureDatabase();
                    CreateDefinitionAccessorCode();
                    CreateMvcCode(target);
                    break;
                case "_def":
                    CreateDefinitionAccessorCode();
                    break;
                case "def":
                    CreateDefinitionAccessorCode();
                    CreateMvcCode(target);
                    break;
                case "mvc":
                    CreateMvcCode(target);
                    break;
                case "css":
                    CreateCssCode();
                    break;
                case "backup":
                    CreateSolutionBackup();
                    break;
                case "test":
                    //TestPerformance(1000, () => Test1(), () => Test2());
                    break;
                default:
                    WriteErrorToConsole(args);
                    break;
            }
            Performances.Record(MethodBase.GetCurrentMethod().Name);
            Performances.PerformanceCollection.Save(Directories.Logs());
            Consoles.Write(Def.Display.CodeDefinerCompleted, Consoles.Types.Success);
            WaitConsole(args);
        }

        private static void Init()
        {
            Performances.Record(MethodBase.GetCurrentMethod().Name);
            Files.DeleteHistoryFile(Directories.Temp(), Def.Parameters.DeleteTempOldThan);
            Files.DeleteHistoryFile(Directories.Histories(), Def.Parameters.DeleteHistoriesOldThan);
            Performances.Record(MethodBase.GetCurrentMethod().Name);
        }

        private static void ValidateArgs(string[] args)
        {
            if (args.Count() == 0)
            {
                WriteErrorToConsole(args);
            }
        }

        private static void ConfigureDatabase()
        {
            Performances.Record(MethodBase.GetCurrentMethod().Name);
            Functions.SqlServer.Configurator.Configure();
            Consoles.Write(Def.Display.CodeDefinerRdsCompleted, Consoles.Types.Success);
            Performances.Record(MethodBase.GetCurrentMethod().Name);
        }

        private static void CreateDefinitionAccessorCode()
        {
            Performances.Record(MethodBase.GetCurrentMethod().Name);
            Functions.AspNetMvc.CSharp.DefinitionAccessorCreator.Create();
            Consoles.Write(Def.Display.CodeDefinerDefCompleted, Consoles.Types.Success);
            Performances.Record(MethodBase.GetCurrentMethod().Name);
        }

        private static void CreateMvcCode(string target)
        {
            Performances.Record(MethodBase.GetCurrentMethod().Name);
            Functions.AspNetMvc.CSharp.MvcCreator.Create(target);
            Consoles.Write(Def.Display.CodeDefinerMvcCompleted, Consoles.Types.Success);
            Performances.Record(MethodBase.GetCurrentMethod().Name);
        }

        private static void CreateCssCode()
        {
            Performances.Record(MethodBase.GetCurrentMethod().Name);
            Functions.Web.Styles.CssCreator.Create();
            Consoles.Write(Def.Display.CodeDefinerCssCompleted, Consoles.Types.Success);
            Performances.Record(MethodBase.GetCurrentMethod().Name);
        }

        private static void CreateSolutionBackup()
        {
            Performances.Record(MethodBase.GetCurrentMethod().Name);
            Functions.CodeDefiner.BackupCreater.BackupSolutionFiles();
            Consoles.Write(Def.Display.CodeDefinerBackupCompleted, Consoles.Types.Success);
            Performances.Record(MethodBase.GetCurrentMethod().Name);
        }

        private static void WaitConsole(string[] args)
        {
            System.Threading.Thread.Sleep(10000);
            if (args.Contains("-r"))
            {
                Console.ReadKey();
            }
        }

        private static void WriteErrorToConsole(IEnumerable<string> args)
        {
            Consoles.Write(
                Def.Display.IncorrectArgument.Params(args.Join(" ")),
                Consoles.Types.Error,
                abort: true);
        }

        [Conditional("DEBUG")]
        private static void TestPerformance(int loopCount, params Action[] actionCollection)
        {
            actionCollection
                .Select((o, i) => new { Count = i + 1, Action = o })
                .ForEach(data =>
                {
                    Performances.Record("Action:" + data.Count);
                    for (int count = 1; count <= loopCount; count++)
                    {
                        data.Action();
                    }
                    Performances.Record("Action:" + data.Count);
                });
        }
    }
}
