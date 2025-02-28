using UnityEngine;
using Zenject;

public class SceneZenjectInstaller : MonoInstaller
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private TruckBody _truckBody;

    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromInstance(_uiManager);
        Container.Bind<TruckBody>().FromInstance(_truckBody);
    }
}
