using Microsoft.EntityFrameworkCore;
using ValuteSalute.Domain.Entities;
using ValuteSalute.Domain.Repositories.Abstract;

namespace ValuteSalute.Domain.Repositories.EntityFramework
{
    public class EFTextFieldsRepository
        : ITextFieldsRepository
    {
        private readonly AppDbContext _appDbContext;

        public EFTextFieldsRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public IQueryable<TextField> GetTextFields()
        {
            return this._appDbContext.TextFields;
        }

        public TextField GetTextFieldById(Guid id)
        {
            return this._appDbContext.TextFields.FirstOrDefault(x => x.Id == id);
        }

        public TextField GetTextFieldByCodeWord(string codeWord)
        {
            return this._appDbContext.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
        }

        public void SaveTextField(TextField entity)
        {
            if (entity.Id == default)
            {
                this._appDbContext.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this._appDbContext.Entry(entity).State = EntityState.Modified;
            }

            this._appDbContext.SaveChanges();
        }

        public void DeleteTextField(Guid id)
        {
            this._appDbContext.Remove(new TextField() { Id = id });

            this._appDbContext.SaveChanges();
        }
    }
}
