Shader "Unlit/BoardShader"
{
  SubShader{
    BindChannels{
    Bind "Color", color
    Bind "Vertex", vertex
    Bind "TexCoord", texcoord
  }
    Pass{
  }
  }
}