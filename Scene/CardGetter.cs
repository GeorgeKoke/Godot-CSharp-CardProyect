using Godot;
using System;

public class CardGetter : Area2D
{
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Position = GetGlobalMousePosition();

        var count = GetOverlappingBodies().Count;
        if(count == 0){
            ;   
        }else if(count == 1){
            var body = (Kard)GetOverlappingBodies()[0];
            body.Call("Chosen");
            if(Input.IsActionJustPressed("mouse_left")){
                GetParent().Call("PushCardtoTop", (Kard)GetOverlappingBodies()[0]);
            }
        }else{
            var max_index = -1;
            Kard top_card = null;

            foreach (var card in GetOverlappingBodies())
            {
                var c = (Kard)card;
                if(c.ZIndex > max_index){
                    max_index = c.ZIndex;
                    top_card = c;
                }
            }
            top_card.Call("Chosen");

            foreach (var card in GetOverlappingBodies())
            {
                var c = (Kard)card;
                if(c != top_card){
                    c.chosen = false;
                }
            }
            if(Input.IsActionJustPressed("mouse_left")){
                GetParent().Call("PushCardtoTop", top_card);
            }

        }

    }
}
