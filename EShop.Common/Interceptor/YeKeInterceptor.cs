using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Eshop.Common.Interceptor
{
    /// <summary>
    /// رهگیری جهت اصلاح حروف ک و ی از عربی به فارسی در درگاه دیتابیس
    /// </summary>
    public class YeKeInterceptor : IDbCommandInterceptor
    {
        public DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
        {
            return result;
        }

        public InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
        {
            return result;
        }

        public void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
        }

        public Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData, InterceptionResult result)
        {
            return result;
        }

        public int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            return result;
        }

        public ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            return new ValueTask<int>(result);
        }

        public InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            command.ApplyCorrectYeKe();
            command.ApplyCorrectNumber();

            return result;
        }

        public ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            command.ApplyCorrectYeKe();
            command.ApplyCorrectNumber();

            return new ValueTask<InterceptionResult<int>>(result);
        }

        public DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            return result;
        }

        public ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            command.ApplyCorrectYeKe();
            command.ApplyCorrectNumber();

            return new ValueTask<DbDataReader>(result);
        }

        public InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command.ApplyCorrectYeKe();
            command.ApplyCorrectNumber();

            return result;
        }

        public ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            command.ApplyCorrectYeKe();
            command.ApplyCorrectNumber();

            return new ValueTask<InterceptionResult<DbDataReader>>(result);
        }

        public object? ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object? result)
        {
            return result;
        }

        public ValueTask<object?> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object? result, CancellationToken cancellationToken = default)
        {
            return new ValueTask<object?>(result);
        }

        public InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            command.ApplyCorrectYeKe();
            command.ApplyCorrectNumber();

            return result;
        }

        public ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            command.ApplyCorrectYeKe();
            command.ApplyCorrectNumber();

            return new ValueTask<InterceptionResult<object>>(result);
        }
    }
}