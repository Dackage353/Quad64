using System.Windows.Forms;

public class MyGLControl : OpenTK.GLControl
{
    protected override bool IsInputKey(Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Up:
            case Keys.Down:
            case Keys.Left:
            case Keys.Right:
                return true;
            default:
                return base.IsInputKey(keyData);
        }
    }
}