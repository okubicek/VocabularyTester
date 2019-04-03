namespace Common.Cqrs
{
    public interface IQuery<TRes, TParam>
    {
		TRes Get(TParam query);
    }
}