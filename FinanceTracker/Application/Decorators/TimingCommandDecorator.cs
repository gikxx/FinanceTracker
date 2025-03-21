using System;
using System.Diagnostics;
using FinanceTracker.Application.Commands;

namespace FinanceTracker.Application.Decorators
{
    public class TimingCommandDecorator : ICommand
    {
        private readonly ICommand _command;

        public TimingCommandDecorator(ICommand command)
        {
            _command = command;
        }

        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            _command.Execute();
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}