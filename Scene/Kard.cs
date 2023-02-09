using Godot;
using System;
using System.Collections.Generic;

public class Kard : KinematicBody2D
{
    //ATRIBUTES
    [Export] Vector2 cardDimension  = new Vector2(0.35f,0.35f);
    float dragginDistance;
    Vector2 dir, zone_pos;
    Vector2 newPosition = Vector2.Zero;
    List<Area2D> list_zone = new List<Area2D>();
    public bool dragging, mouse_in, chosen, on_zone = false;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Scale = cardDimension;
        zone_pos = Vector2.Zero;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(dragging){
            DragAnim(delta);
            MoveAndSlide((newPosition - Position)* new Vector2(30,30));
            // MoveAndSlide(Position.LinearInterpolate(newPosition - Position, 15 *delta));
        }else{
            if(on_zone){
                ZonePosition();
                GlobalPosition = GlobalPosition.LinearInterpolate(zone_pos, 15 * delta);
            }
            DropAnim(delta);
        }
    }

    public override void _Input(InputEvent @event){
        if(@event is InputEventMouseButton){
            if(chosen && @event.IsActionPressed("mouse_left") && mouse_in){
                dragginDistance = Position.DistanceTo(GetGlobalMousePosition());
                dir = (GetGlobalMousePosition() - Position).Normalized();
                dragging = true;
                newPosition = newPosition.LinearInterpolate(GetGlobalMousePosition() - dragginDistance * dir, 1);
                // Sin Interpolacion de Movimiento
                // newPosition = GetGlobalMousePosition() - dragginDistance * dir; 
            }else{
                dragging = false;
                chosen = false;
            }
        }else if(@event is InputEventMouseMotion){
            if(dragging){
                newPosition = newPosition.LinearInterpolate(GetGlobalMousePosition() - dragginDistance * dir, 1);
                // Sin Interpolacion de Movimiento
                // newPosition = GetGlobalMousePosition() - dragginDistance * dir; 
                
            }
        }
    }
    private void ZonePosition(){
        if(list_zone.Count > 0){
            zone_pos = list_zone[0].GlobalPosition;
        }else{
            zone_pos = Vector2.Zero;
        }
    }
    public void Chosen(){
        chosen = true;
        // GD.Print(ZIndex);
    }

    // Some Card Animation using LinearInterpolate to scale 
    private void DragAnim(float delta){
        Scale = Scale.LinearInterpolate((cardDimension + new Vector2(0.1f,0.1f)), 25* delta);
    }
    private void DropAnim(float delta){
        Scale = Scale.LinearInterpolate(cardDimension, 25* delta);
    }


    //Conected Signals
    public void _on_Kard_mouse_entered(){
        mouse_in = true;
    }
    public void _on_Kard_mouse_exited(){
        mouse_in = false;
    }

    public void _on_CardArea_area_entered(Area2D area2D){
        if(area2D.GetParent().IsInGroup("zone")){
            on_zone = true;
            list_zone.Add(area2D);
        }   
    }
    public void _on_CardArea_area_exited(Area2D area2D){
        if(list_zone.Contains(area2D)){
            list_zone.Remove(area2D);
        }
        if(list_zone.Count == 0){
            on_zone = false;
        }

    }


}
