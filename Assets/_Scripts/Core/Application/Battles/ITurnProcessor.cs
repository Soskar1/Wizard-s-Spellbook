using System.Threading.Tasks;
using WizardsSpellbook.Core.Domain.Battles;

namespace WizardsSpellbook.Core.Application.Battles
{
    public interface ITurnProcessor
    {
        public Task<TurnResult> StartTurn();
    }
}