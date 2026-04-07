using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardsSpellbook.Core.Application.Bootstrap
{
    public class RootBootstrap : MonoBehaviour
    {
        public void Start()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
