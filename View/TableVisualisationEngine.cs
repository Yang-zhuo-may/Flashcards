using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        internal static void ShowCardTable(List<CardSession> cards)
        {
            if (cards == null || !cards.Any())
            {
                AnsiConsole.Markup("[red]No data to display.[/]");
                return;
            }

            var table = new Table()
            .Border(TableBorder.Simple)
            .AddColumn("Id")
            .AddColumn("Front")
            .AddColumn("Back");

            int id = 1;
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
    }
}
