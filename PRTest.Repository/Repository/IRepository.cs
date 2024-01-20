using System.Linq.Expressions;

namespace PRTest.Repository.Repository
{
	public interface IRepository<T> where T : class
	{
		void Commit();
		void Rollback();
		Task<IEnumerable<T>> GetAll();
		Task<T> GetByID(long Id);
		Task<dynamic> Add(T entity);
		Task<dynamic> Update(T entity);
		Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
		IQueryable<T> GetByCompanyID(Expression<Func<T, bool>> predicate);
		Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
	}
}
