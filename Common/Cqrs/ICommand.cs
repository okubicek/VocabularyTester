namespace Common.Cqrs
{
    public interface ICommand<TRes, TParam>
    {
		TRes Execute(TParam command);
	}

	public interface ICommand<TParam>
	{
		void Execute(TParam command);
	}
}
