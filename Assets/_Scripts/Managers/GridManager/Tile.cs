using UnityEngine;


public enum State
{
    Standart,
    Selected,
    Variant
}

public class Tile : MonoBehaviour
{
    public Color StandartColor;
    public Color HoverColor;

    public Color AttackColor;
    public Color SelectedColor;
    public Color MoveColor;

    [SerializeField] private MeshRenderer _meshRenderer;

    public GameObject Unit;

    [HideInInspector] public Vector2 Position;
    [HideInInspector] public State State;



    public GridInteractor GridInteractor;

    public void ChangeColor(Color color)
    {
        var tempMaterial = new Material(_meshRenderer.sharedMaterial);
        tempMaterial.color = color;
        _meshRenderer.material = tempMaterial;
    }


    private void OnMouseEnter()
    {
        if (State == State.Standart)
        {
            ChangeColor(StandartColor);
        }

        ChangeColor(HoverColor);
    }

    private void OnMouseExit()
    {

        if (State == State.Standart)
        {
            ChangeColor(StandartColor);
        }
        else if (State == State.Variant)
        {
            if (Unit == null)
            {
                ChangeColor(MoveColor);
            }
            else
            {
                ChangeColor(AttackColor);
            }
        }

    }

    private void OnMouseDown()
    {
        if (State == State.Selected)
        {
            GridInteractor.DeselectTile(this);
        }
        //else if (State == State.Variant)
        //{
        //    GridInteractor.GridRepository.SelectedTile.Unit.GoToTile(this);
        //}
        else if (State == State.Standart)
        {
            if (GridInteractor.GridRepository.SelectedTile != null)
            {
                GridInteractor.DeselectTile(GridInteractor.GridRepository.SelectedTile);
            }

            GridInteractor.SelectTile(this);
        }
    }
}
