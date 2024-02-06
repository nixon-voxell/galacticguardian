public class UiManager : SingletonMono<UiManager>
{
    public UiMono[] Uis;

    public void SetOnlyVisible<T>()
    where T : UiMono
    {
        foreach (UiMono ui in this.Uis)
        {
            if (ui.GetType() == typeof(T))
            {
                ui.Root.visible = true;
            }
            else
            {
                ui.Root.visible = false;
            }
        }
    }

    public T GetUi<T>() where T : UiMono
    {
        foreach (UiMono ui in this.Uis)
        {
            if (ui.GetType() == typeof(T))
            {
                return ui as T;
            }
        }

        return null;
    }
}
