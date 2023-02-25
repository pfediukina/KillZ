﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IState
{
    public NetworkInputData Data { get; set; }
    public void Enter();
    public void Exit();
    public void Update();
}
