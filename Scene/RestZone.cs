using Godot;
using System;

public class RestZone : Node2D
{
    bool entered = false;
    Sprite zone;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        zone = GetNode<Sprite>("Sprite");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(entered){
            zone.Modulate = Color.Color8(255,0,0,255);
        }else{
            zone.Modulate = Color.Color8(255,255,255,255);
        }
    }

    public void _on_Area2D_area_entered(Area2D area2D){
        entered = true;
    }
    public void _on_Area2D_area_exited(Area2D area2D){
        entered = false;
    }
}
