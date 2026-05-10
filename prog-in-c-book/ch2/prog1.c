#include <stdio.h>

int main (void)
{
      float  f1 = 123.125, f2;
      int    i1, i2 = -150;
      char      c = 'a';

      printf("f1=%f, i1=%i, i2=%i, c=%c\n", f1, i1, i2, c);

      i1 = f1;                 // floating to integer conversion
      printf ("%f assigned to an int produces %i\n", f1, i1);

      f1 = i2;                 // integer to floating conversion
      printf ("%i assigned to a float produces %f\n", i2, f1);

      f1 = i2 / 100;           // integer divided by integer
      printf ("%i divided by 100 produces %f\n", i2, f1);

      f2 = i2 / 100.0;           // integer divided by a float
      printf ("%i divided by 100.0 produces %f\n", i2, f2);

      f2 = (float) i2 / 100;     // type cast operator
      printf ("(float) %i divided by 100 produces %f\n", i2, f2);

      return 0;
}
