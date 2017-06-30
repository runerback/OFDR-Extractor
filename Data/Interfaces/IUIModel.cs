
namespace Extractor.Data
{
	public interface IUIModel
	{
		object Clone();
		void UpdateBy(object other);
	}
}
