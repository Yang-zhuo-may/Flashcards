using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.DTOs;
using Flashcards.Modes;
using Spectre.Console;

namespace Flashcards.View
{
    internal class TableVisualisationEngine
    {
        internal static void ShowStackTable(List<TaskSession> stacks)
        {
            if (stacks == null || !stacks.Any())
            {
                AnsiConsole.Markup("[red]No data to display.[/]");
                Console.WriteLine();
                return;
            }

            var table = new Table()
            .Border(TableBorder.Simple)
            .AddColumn("Id")
            .AddColumn("Stack Name");

            foreach (var session in stacks)
            {
                table.AddRow(
                    new Markup($"{session.Id}"),
                    new Markup($"[green]{session.Name}[/]")
                    );
            }

            AnsiConsole.Write(table);
        }

        internal static void ShowCardTable(List<CardDTO> cards)
        {
            if (cards == null || !cards.Any())
            {
                AnsiConsole.Markup("[red]No data to display.[/]");
                Console.WriteLine();
                return;
            }

            var table = new Table()
            .Border(TableBorder.Simple)
            .AddColumn("Id")
            .AddColumn("Front")
            .AddColumn("Back");

            int id = 0;
            foreach (var session in cards)
            {
                id += 1;
                table.AddRow(
                    new Markup($"{id}"),
                    new Markup($"[green]{session.Front}[/]"),
                    new Markup($"[green]{session.Back}[/]")
                    );
            }

            AnsiConsole.Write(table);
        }

        internal static void ShowSingleCard(string header,string content)
        {
            if (content == null)
            {
                AnsiConsole.Markup("[red]It's a empty card![/]");
                Console.WriteLine();
                return;
            }

            var table = new Table()
            .Border(TableBorder.Simple)
            .AddColumn($"{header}");

            table.AddRow(
                new Markup($"[green]{content}[/]")
                );

            AnsiConsole.Write(table);
        }

        internal static void ShowRecord(List<StudySessionSession> sessions)
        {
            Console.Clear();
            if (sessions == null || !sessions.Any())
            {
                AnsiConsole.Markup("[red]No data to display.[/]");
                Console.WriteLine();
                return;
            }

            var table = new Table().Border(TableBorder.Simple)
            .AddColumn("Id")
            .AddColumn("Date")
            .AddColumn("Score")
            .AddColumn("Stack");

            foreach (var session in sessions)
            {
                table.AddRow(
                    new Markup($"{session.StackId}"),
                    new Markup($"[green]{session.Date}[/]"),
                    new Markup($"[green]{session.Score}[/]"),
                    new Markup($"[green]{session.StackName}[/]")
                    );
            }

            AnsiConsole.Write(table);
        }
    }
}
