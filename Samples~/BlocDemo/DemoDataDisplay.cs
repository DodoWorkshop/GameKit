using UnityEngine;
using UnityEngine.UI;

namespace DodoWorkshop.GameKit.Demos.Bloc
{
	public class DemoDataDisplay : MonoBehaviour, IWithBlocs
	{
        [Header("UI")]
        [SerializeField]
        private Text counterText;

        [SerializeField]
        private Text titleText;

        [SerializeField]
        private Text errorText;


        [InjectBloc]
        private DemoDataBloc bloc;


        public void IncrementCount()
        {
            bloc.Send(new DemoDataBlocCommands.IncrementCount());
        }

        public void ChangeName(string name)
        {
            bloc.Send(new DemoDataBlocCommands.ChangeName(name));
        }

        public void ResetBloc()
        {
            bloc.ResetState();
        }

        private void FetchData(DemoData data)
        {
            titleText.text = data.Name;
            counterText.text = data.Count.ToString();
        }

        private void OnBlocUpdate(BlocUpdate<IDemoDataBlocCommand, DemoData> update)
        {
            if (update.IsErrored)
            {
                errorText.gameObject.SetActive(true);
                errorText.text = update.Exception.Message;
            }
            else
            {
                errorText.gameObject.SetActive(false);
            }

            FetchData(update.State);
        }

        private void AddListeners()
        {
            bloc.OnStateUpdated += OnBlocUpdate;
        }

        private void Awake()
        {
            this.LoadBlocs();

            FetchData(bloc.State);
            errorText.gameObject.SetActive(false);

            AddListeners();
        }
    }
}
