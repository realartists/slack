namespace RealArtists.Slack {
  using System;

  public static class ReflectionUtils {
    internal static bool IsNullableType(Type t) {
      return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
  }
}
