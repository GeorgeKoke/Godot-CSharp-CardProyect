using Godot;
using System;
using System.Collections.Generic;

public class Card : Node2D
{
    bool selected = false;
    bool on_zone = false;
    List<Area2D> list_zone = new List<Area2D>(); 
    Vector2 zone_pos;
    private Sprite card; //Para declarar un Nodo dentro del mismo arbol

    public override void _Ready(){
        card = GetNode<Sprite>("Sprite"); //Se asigna el nodo del arbol en OnReady;
        zone_pos = Vector2.Zero;
    }

    public override void _Process(float delta)
    {
        if(selected){
            DragAnim(delta);
            GlobalPosition = GlobalPosition.LinearInterpolate(GetGlobalMousePosition(), 25 * delta);
        }else if(!selected){
            if(on_zone){
                ZonePosition();
                GlobalPosition = GlobalPosition.LinearInterpolate(zone_pos, 15 * delta);
            }
            DropAnim(delta);
        }
    }

    private void ZonePosition(){
        if(list_zone.Count > 0){
            zone_pos = list_zone[0].GlobalPosition;
        }else{
            zone_pos = Vector2.Zero;
        }
    }

    private void DragAnim(float delta){
        card.Scale = card.Scale.LinearInterpolate(new Vector2(0.5f,0.5f), 25* delta);
    }
    private void DropAnim(float delta){
        card.Scale = card.Scale.LinearInterpolate(new Vector2(0.4f,0.4f), 25* delta);
    }

//CONECTED SIGNALS
    public void _on_Area2D_input_event(Node n, InputEvent @event, int idx){
        if(Input.IsActionJustPressed("mouse_left")){
            selected = true;
            
        }else if(Input.IsActionJustReleased("mouse_left")){
            selected = false;
        }
    }

    public void _on_Area2D_area_entered(Area2D area2D){
        if(area2D.GetParent().IsInGroup("zone")){
            on_zone = true;
            list_zone.Add(area2D);
        }   
    }
    public void _on_Area2D_area_exited(Area2D area2D){
        if(list_zone.Contains(area2D)){
            list_zone.Remove(area2D);
        }
        if(list_zone.Count == 0){
            on_zone = false;
        }

    }
}
