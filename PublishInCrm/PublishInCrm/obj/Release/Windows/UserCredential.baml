   M S B A M L   `   `   `  ����   PublishInCrm4    .CemYabansu.PublishInCrm.Windows.UserCredential   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls     AccentColorBrush#���   $��1q�  � Title$  USER CREDENTIALS��$	��493��$	��449�� � 
ResizeMode$ NoResize=� � 	Resources  �� MergedDictionaries �� Source��  �� $H Bpack://application:,,,/PublishInCrm;component/Resources/Icons.xaml?�%    .��� .+��� $��CRM Connection��$
��Left=�$��5,10,0,0q�$	��Top=�$��43��$��20(�$	��180��$
��Bold"��� -    ��Name$ 
SaveButton��$
��Save��$
��Left=�$��	5,426,0,0q�$	��Top=�$��68��$��29��{�.  �� -   $��Cancel��$
��Left=�$��
78,426,0,0q�$	��Top=�$��85��$��29���� -    ��Name$ ConnectionStatusLabel��$
��Left=�$��235,427,-3,0q�$	��Top=�$	��209��$��26���� $��SERVER INFORMATION��$
��Left=�$��	5,58,-3,0q�$	��Top=�$	��110��$	��439��.��� $
��Left=�$	��429��$��0,0,0,0q�.+��� $��	Server		:��$
��Left=�$��2,5,0,0q�$	��Top=�$	��111���� -    ��Name$ ServerTextBox��$��Center=�$
��Left=�$��23��$��	118,9,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��311���� $��Port		:��$
��Left=�$��2,36,0,0q�$	��Top=�$	��111���� -   $ PortTextBox��$��Center=�$
��Left=�$��23��$��
118,40,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��101���� -   	 ��Name$	 SslCheckBox��$��Use SSL��$
��Left=�$��
238,44,0,0q�$	��Top=��� $��USER INFORMATION��$
��Left=�$	��Top=�$��
5,173,-3,0q�$	��147��$	��439��.��� $
��Left=�$	��427��.+��� $��	Domain		:��$
��Left=�$��0,7,0,0q�$	��Top=�$	��111���� -   $ DomainTextBox��$��Center=�$
��Left=�$��23��$��
116,10,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��311���� $��
Username	:��$
��Left=�$��0,38,0,0q�$	��Top=�$	��111���� -   $ UsernameTextBox��$��Center=�$
��Left=�$��23��$��
116,42,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��311���� $��
Password	:��$
��Left=�$��0,72,0,0q�$	��Top=�$	��111��G� -	   
 G�Name$
 PasswordTextBox��$��Center=�$
��Left=�$��23��$��
116,76,0,0q�$	��Top=�$	��311���� $��ORGANIZATIONS��$
��Left=�$��
5,325,-3,0q�$	��Top=�$	��439��$��86��.��� $
��Left=�$	��427��$��55��$	��Top=�.+��� -
    ��Name$ OrganizationsComboBox��$��Center=�$
��Left=�$��0,10,0,0q�$	��Top=�$	��221��$��29��{�.  )  #MahApps.Metro.Controls.ProgressRing  -     Name$ ProgressRing��   IsActive .  $��30��$��226,9,164,16q�$��30���� -   $ GetOrganizationsButton��$��Get Organizations��$
��Left=�$��
268,10,0,0q�$	��Top=�$	��159��$��29��  -   $ ConnectionProgressRing�� .  $��177,426,0,0q�$��30��$��30��$
��Left=�$	��Top=�   IsLarge .  