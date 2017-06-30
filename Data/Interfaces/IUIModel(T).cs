
namespace Extractor.Data
{
	public interface IUIModel<T>
	{
		T Clone();
		void UpdateBy(T other);
	}
}
