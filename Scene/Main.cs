using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class Main : Node
{
    public static List<Kard> card_stack = new List<Kard>(10);

    PackedScene card_scene = ResourceLoader.Load<PackedScene>("res://Scene/Kard.tscn");
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (var i in Enumerable.Range(0,5))
        {
            var card = (Kard)card_scene.Instance();
            AddChild(card);
            AddCard(card);
        }
    }
    public void AddCard(Kard card){
        card_stack.Add(card);

        var count = 0;
        foreach (var item in card_stack){
            item.ZIndex = count;
            count ++;
        }
    }

    public void PushCardtoTop(Kard card){
        card_stack.Remove(card);
        AddCard(card);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
