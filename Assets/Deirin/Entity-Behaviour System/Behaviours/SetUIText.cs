namespace Deirin.EB
{
    public class SetUIText : BaseBehaviour
    {
        public UnityEngine.UI.Text TextElement;

        public void SetEnum(System.Enum value)
        {
            TextElement.text = value.ToString();
        }

        public void SetString(string value)
        {
            TextElement.text = value;
        }

        public void SetFloat(float value)
        {
            TextElement.text = value.ToString();
        }
    }
}