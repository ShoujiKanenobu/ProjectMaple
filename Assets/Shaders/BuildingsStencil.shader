Shader "BuildingsStencil" {

    Properties{
        _Color("Color", Color) = (1,1,1)
    }

        SubShader{
            Color[_Color]
            Pass
            {
                Stencil
                {
                    Ref 1
                    Comp NotEqual
                }
            }
    }
}

