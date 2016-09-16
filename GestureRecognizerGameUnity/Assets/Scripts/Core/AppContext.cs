using Core.Api;
using Core.Impl;
using Logic.Commands;
using Logic.Signals;
using Model.Api;
using Model.Impl;
using UIView.Mediator;
using UIView.Windows;
using UnityEngine;

namespace Core
{
    public class AppContext : SignalContext
    {
        public AppContext(MonoBehaviour view) : base(view)
        {
        }

        public override void Launch()
        {
            base.Launch();
            injectionBinder.GetInstance<AppStartSignal>().Dispatch();
            Debug.Log("launched");
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            // model layer
            injectionBinder.Bind<IGestureInput>().To<GestureInputContext>().ToSingleton();
            injectionBinder.Bind<IGameFlowModel>().To<GameFlowModel>().ToSingleton();
            injectionBinder.Bind<IGestureTemplatesModel>().To<GestureTemplatesModel>();
            injectionBinder.Bind<IPlaySessionModel>().To<PlaySessionModel>();

            // view layer
            mediationBinder.Bind<MainMenuWindow>().To<MainMenuWindowMediator>();
            mediationBinder.Bind<GamePlayHud>().To<GamePlayHudMediator>();
            mediationBinder.Bind<PauseWindow>().To<PauseWindowMediator>();
            mediationBinder.Bind<GameOverWindow>().To<GameOverWindowMediator>();

            // logic layer
            commandBinder.Bind<AppStartSignal>().To<AppStartCommand>().To<BindInputCommand>().Once();

            commandBinder.Bind<ChangeGameFlowStateSignal>().To<ChangeGameFlowStateCommand>();
            commandBinder.Bind<RestartGamePlaySignal>().To<RestartGamePlayCommand>();

            commandBinder.Bind<GestureStartSignal>().To<GestureStartCommand>();
            commandBinder.Bind<GestureEndSignal>().To<GestureEndCommand>();

            commandBinder.Bind<GestureRendererCreateSignal>().To<CreateGestureRendererCommand>();
            commandBinder.Bind<GestureRendererClearSignal>().To<ClearGestureRenderersCommand>();
            commandBinder.Bind<GestureRendererUpdateSignal>().To<UpdateGestureRendererCommand>();

            Debug.Log("Bind finished");
        }
    }
}