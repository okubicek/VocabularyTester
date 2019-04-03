namespace Common.Cqrs
{
    public interface ICommand<TRes, TParam>
    {
		TRes Execute(TParam command);
    }
}
