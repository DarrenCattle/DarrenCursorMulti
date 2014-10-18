private void Panel0_Click(object sender, RoutedEventArgs e)
{
    switch (state[0])
    {
        case '0': //0: no charge, not charging
            state[0] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[0] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[0] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[0] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[0] = '3';
            start = DateTime.Now;
            break;
    }
}
private void Panel1_Click(object sender, RoutedEventArgs e)
{
    switch (state[1])
    {
        case '0': //0: no charge, not charging
            state[1] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[1] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[1] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[1] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[1] = '3';
            start = DateTime.Now;
            break;
    }
}
private void Panel2_Click(object sender, RoutedEventArgs e)
{
    switch (state[2])
    {
        case '0': //0: no charge, not charging
            state[2] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[2] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[2] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[2] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[2] = '3';
            start = DateTime.Now;
            break;
    }
}
private void Panel0_Click(object sender, RoutedEventArgs e)
{
    switch (state[0])
    {
        case '0': //0: no charge, not charging
            state[0] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[0] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[0] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[0] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[0] = '3';
            start = DateTime.Now;
            break;
    }
}
private void Panel0_Click(object sender, RoutedEventArgs e)
{
    switch (state[0])
    {
        case '0': //0: no charge, not charging
            state[0] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[0] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[0] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[0] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[0] = '3';
            start = DateTime.Now;
            break;
    }
}
private void Panel0_Click(object sender, RoutedEventArgs e)
{
    switch (state[0])
    {
        case '0': //0: no charge, not charging
            state[0] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[0] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[0] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[0] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[0] = '3';
            start = DateTime.Now;
            break;
    }
}
private void Panel0_Click(object sender, RoutedEventArgs e)
{
    switch (state[0])
    {
        case '0': //0: no charge, not charging
            state[0] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[0] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[0] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[0] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[0] = '3';
            start = DateTime.Now;
            break;
    }
}
private void Panel0_Click(object sender, RoutedEventArgs e)
{
    switch (state[0])
    {
        case '0': //0: no charge, not charging
            state[0] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[0] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[0] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[0] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[0] = '3';
            start = DateTime.Now;
            break;
    }
}
private void Panel0_Click(object sender, RoutedEventArgs e)
{
    switch (state[0])
    {
        case '0': //0: no charge, not charging
            state[0] = '1';
            start = DateTime.Now;
            break;
        case '1': //1: partially charged, charging
            state[0] = '2';
            hold += millis;
            break;
        case '2': //2: partially charged, holding
            state[0] = hold >= 120000 ? '1' : '3';
            start = DateTime.Now;
            break;
        case '3': //3: partially charged, discharging
            state[0] = '2';
            hold -= millis;
            millis = 0;
            break;
        case '4': //4: fully charged, not charging
            state[0] = '3';
            start = DateTime.Now;
            break;
    }
}