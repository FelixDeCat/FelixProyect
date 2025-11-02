
#region Module
/// <summary> 
/// ---- M O D U L E  ----
/// <para>IModule sirve para tener un Manager que simplemente Agrupe todos los IModule, 
/// Luego el manager se encargue  de discriminar si es un 
/// <see cref="IPausable"/>, 
/// <see cref="IActivable"/>, 
/// <see cref="IReseteable"/>, 
/// <see cref="IUpdateable"/>, 
/// <see cref="IFixedUpdateable"/> </para>
/// </summary>
public interface IModule { }
#endregion

#region Initializable
/// <summary>
/// ---- S T A R T E A B L E  ----
/// <para> Un Start para que vaya de la mano 
/// con el Start de quien lo implementó... </para>
/// <inheritdoc/>
/// </summary>
public interface IStarteable : IModule
{
    public void Start();
}
#endregion

#region Activable
/// <summary>
/// ---- A C T I V A B L E  ----
/// <para> Un Activable es independiente de un IPausable porque el 
/// pausable se activa o desactiva en grupo; el activable lo gestiona el objeto.</para>
/// <para>tambien es un... </para>
/// <inheritdoc/>
/// </summary>
public interface IActivable : IModule
{
    public void Active();
    public void Deactivate();
}
#endregion

#region Pausable
/// <summary>
/// ---- P A U S A B L E  ----
/// <para> Un Pausable es independiente de un Activable porque el pausable 
/// se activa o desactiva en grupo; el activable lo gestiona el objeto.</para>
/// <para>tambien es un...</para>
/// <inheritdoc/>
/// </summary>
public interface IPausable : IModule
{
    public void Pause();
    public void Resume();
}
#endregion

#region Reseteable
/// <summary>
/// ---- R E S E T E A B L E  ----
/// <para> Un reseteable es para reiniciar valores por defecto.</para>
/// <para>tambien es un...</para>
/// <inheritdoc/>
/// </summary>
public interface IReseteable : IModule
{
    public void ResetModule();
}
#endregion

#region Updateable
/// <summary>
/// ---- U P D A T E A B L E  ----
/// <para> Un Updateable sirve para que se pueda Actualizar desde afuera
/// si es un script que no herede de Monovehabiour.</para>
/// <para>tambien es un...</para> 
/// <inheritdoc/>
/// </summary>
public interface IUpdateable : IModule
{
    public void Tick(float delta);
}
#endregion

#region FixedUpdateable
/// <summary>
/// ---- F I X E D  U P D A T E A B L E  ----
/// <para> Un FixedUpdateable sirve para que se pueda Actualizar desde afuera
/// si es un script que no herede de Monovehabiour.</para>
/// <para>tambien es un...</para> 
/// <inheritdoc/>
/// </summary>
public interface IFixedUpdateable : IModule
{
    public void FixedTick(float delta);
}
#endregion