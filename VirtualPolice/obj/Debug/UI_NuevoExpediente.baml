   M S B A M L   `   `   `  ����  VirtualPolice&     VirtualPolice.UI_NuevoExpediente   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Crear Expediente��5   	   $	��654��6"   $	��761��6/    � WindowStartupLocation$ CenterScreen=�6;    � 
ResizeMode$ NoResize=�6`   ��5      �� 5   
    �� ImageSource$ Images/FondoApp.jpg��6    j� Stretch$ UniformToFill=�67   .��5      � .+�5	   
   �� -   5         ��Name$ l_DNI��5	      $��DNI: ��6   $
��Left=�6/   $��	33,25,0,0q�6J   $	��Top=�6]   $��43��6u   $��26��6�   �� 5
   
   -   5         ��Name$ tb_DNI��5
      $
��Left=�6"   $��23��6=   $��
176,28,0,0q�6I    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $	��120��6�    �� 	MaxLength$ 9��6�   �� 5   
   -   5        $ l_nombre��5      $��NOMBRE: ��6"   $
��Left=�65   $��	33,56,0,0q�6P   $	��Top=�6c   $��26��6{   $��66��6�   �� 5   
   -   5        $ l_apellidos��5      $��APELLIDOS: ��6%   $
��Left=�6;   $��	33,87,0,0q�6V   $	��Top=�6i   $��26��6�   $��75��6�   �� 5   
   -   5        $ l_fechaNacimiento��5      $��FECHA  NACIMIENTO:��6+   $
��Left=�6H   $��
33,118,0,0q�6c   $	��Top=�6w   $��26��6�   $	��126��6�   �� 5   
   -   5        $ l_sexo��5      $��SEXO:��6    $
��Left=�60   $��
33,149,0,0q�6K   $	��Top=�6_   $��26��6w   $��42��6�   �� 5   
   -   5        	 ��Name$	 gb_direccion��5      $��DIRECCIÓN POSTAL��6)   $
��Left=�6C   $��
33,190,0,0q�6^   $	��Top=�6r   $	��208��6�   $	��305��6�   .��5      � .+�5      �� -	   5        $ l_calle��5      $��CALLE: ��6)   $��48��6;   $��Center=�6F   $
��Left=�6j   $	��Top=�6�   �� 5      -
   5        $ l_numero��5      $��NUMERO:��6*   $
��Left=�6<   $	��Top=�6W   $��0,26,0,0q�6o   �� 5      -   5        $ l_piso��5      $��PISO:��6(   $
��Left=�68   $	��Top=�6S   $��0,52,0,0q�6k   �� 5      -   5        $ l_localidad��5      $��
LOCALIDAD:��6-   $
��Left=�6B   $	��Top=�6]   $��0,78,0,0q�6u   �� 5      -   5        $ l_provincia��5      $��
PROVINCIA:��6-   $
��Left=�6B   $	��Top=�6]   $��	0,104,0,0q�6u   �� 5      -   5        $
 l_cp��5      $	��CP:��6&   $
��Left=�64   $	��Top=�6O   $��	0,130,0,0q�6g   �� 5      -   5        $ l_pais��5      $��PAIS:��6(   $
��Left=�68   $	��Top=�6S   $��	0,157,0,0q�6k   �� 5      -   5        $ tb_calle��5      $
��Left=�6,   $��23��6G   $��	138,3,0,0q�6S   $
 Wrap=�6f   $	��Top=�6z   $	��120��6�   �� 5      -   5        $ 	tb_numero��5      $
��Left=�6-   $��23��6H   $��
138,29,0,0q�6T   $
 Wrap=�6h   $	��Top=�6|   $	��120��6�   �� 5      -   5        $ tb_piso��5      $
��Left=�6+   $��23��6F   $��
138,55,0,0q�6R   $
 Wrap=�6f   $	��Top=�6z   $	��120��6�   �� 5      -   5        $ tb_localidad��5      $
��Left=�60   $��23��6K   $��
138,81,0,0q�6W   $
 Wrap=�6k   $	��Top=�6   $	��120��6�   �� 5      -   5        $ tb_provincia��5      $
��Left=�60   $��23��6K   $��138,106,0,0q�6W   $
 Wrap=�6l   $	��Top=�6�   $	��120��6�   �� 5      -   5        $ tb_cp��5      $
��Left=�6)   $��23��6D   $��138,132,0,0q�6P   $
 Wrap=�6e   $	��Top=�6y   $	��120��6�   �� 5      -   5        $ tb_pais��5      $
��Left=�6+   $��23��6F   $��138,160,0,-0.6q�6R   $
 Wrap=�6j   $	��Top=�6~   $	��120��6�   5       5!      �� 5"   
   -   5        $ 	tb_nombre��5"      $
��Left=�6%   $��23��6@   $��
176,59,0,0q�6L   $
 Wrap=�6`   $	��Top=�6t   $	��193��6�   �� 5#   
   -   5        $ tb_fechaNacimiento��5#      $
��Left=�6.   $��23��6I   $��176,121,0,0q�6U   $
 Wrap=�6j   $	��Top=�6~   $	��138��6�   �� 5$   
   -   5        $ tb_apellidos��5$      $
��Left=�6(   $��23��6C   $��
176,91,0,0q�6O   $
 Wrap=�6c   $	��Top=�6w   $	��367��6�   �� 5%   
   -   5        
 ��Name$
 bt_confirmarExpediente��5%      $��	Confirmar��61   $
��Left=�6E   $��591,524,0,0q�6`   $	��Top=�6u   $	��132��6�   $��29��6�   6�   �� 5&   
   -   5        $
 bt_cancelarExpediente��5&      $��Cancelar��60   $
��Left=�6C   $��591,575,0,0q�6^   $	��Top=�6s   $	��132��6�   $��29��6�   6�   �� 5'   
   $
��Left=�6   $��33��60   $��175,148,0,0q�6<   $	��Top=�6Q   $��67��6i   .+�5(      	� -   5         	�Name$ rb_sexoHombre��5(      $��Hombre��61   $
��Left=�6B   $	��Top=�6]   $��15��6u   $��62��6�   	� 5)      -   5        $ rb_sexoMujer��5)      $��Mujer��60   $
��Left=�6@   $	��Top=�6[   $��15��6s   $��50��6   5*      �� 5+   
   $��Foto de expediente��6   $
��Left=�6-   $��	605,5,0,0q�6H   $	��Top=�6[   $
��Bold"�6s   �� 5,   
   -   5        $��cuadro_foto��5,      $
��Left=�6'   $	��115��6B   $��
605,29,0,0q�6O   
���   �$	��Top=�6r   $	��100��6�   �� 5-   
   -   5         ��Name$ i_fotoDelincuente��5-      $
��Left=�6+   $	��115��6F   $��
605,29,0,0q�6S   $	��Top=�6g   $	��100��6   �� 5.   
   -    5        $	 gb_otrosDatos��5.      $#��DATOS ADICIONALES A RELLENAR:��6*   $
��Left=�6Q   $��377,202,0,0q�6l   $	��Top=�6�   $	��135��6�   $	��305��6�   .��5/      � $��	1,3,-1,-3q�6   .+�50      �� -!   5        $ l_altura��50      $��ALTURA:��6*   $��56��6<   $��Center=�6G   $
��Left=�6k   $	��Top=�6�   �� 51      -"   5        $ l_peso��51      $��PESO:��6(   $
��Left=�68   $	��Top=�6S   $��0,26,0,0q�6k   �� 52      -#   5        $ 
l_telefono��52      $��TELÉFONO DE CONTACTO: *��6,   $
��Left=�6N   $	��Top=�6i   $��0,52,0,0q�6�   �� 53      -$   5        $ 	tb_altura��53      $
��Left=�6-   $��23��6H   $��	162,3,0,0q�6T   $
 Wrap=�6g   $	��Top=�6{   $	��120��6�   $ 5��6�   �� 54      -%   5        $ tb_peso��54      $
��Left=�6+   $��23��6F   $��
162,29,0,0q�6R   $
 Wrap=�6f   $	��Top=�6z   $	��120��6�   $ 7��6�   �� 55      -&   5        $ tb_telefono��55      $
��Left=�6/   $��23��6J   $��
162,0,0,35q�6V   $
 Wrap=�6j   $��Bottom=�6~   $	��120��6�   $ 14��6�   �� 56      -'   5        $ estadoCivil��56      $��ESTADO CIVIL:��6-   $
��Left=�6E   $��0,81,0,0q�6`   $	��Top=�6r   $��26��6�   $��86��6�   �� 57      -(   5         ��Name$ cb_estadoCivil��57      $
��Left=�63   $��
162,81,0,0q�6N   $	��Top=�6b   $	��120��6z   $��22��6�   .@�58      �� $��Soltero��6#   �� 59      $��Casado��6#   �� 5:      $��Viudo��6#   5;      5<      5=      �� 5>   
   -)   5        $��Examinar��5>      $
��Left=�6$   $��610,149,0,0q�6?   $	��Top=�6T   $��90��6l   $��20��6w   6�   �� 5?   
   -*   5        $��Borrar foto��5?      $
��Left=�6'   $��610,175,0,0q�6B   $	��Top=�6W   $��90��6o   $��20��6z   6�   �� 5@   
   -+   5        $	 gb_nuevoDenuncia��5@      $��NUEVA DENUNCIA:��6-   $
��Left=�6F   $��
33,408,0,0q�6a   $	��Top=�6u   $	��170��6�   $	��512��6�   .��5A      � $
��Left=�6   $	��497��6.   .+�5B      �� -,   5        $ l_idDenuncia��5B      $	��ID:��6.   $��23��6<   $��Center=�6G   $
��Left=�6k   $	��Top=�6�   �� 5C      --   5        $ l_tipoDenuncia��5C      $��TIPO:��60   $
��Left=�6@   $	��Top=�6[   $��0,26,0,0q�6s   �� 5D      -.   5        $ l_fechaDenuncia��5D      $��FECHA:��61   $
��Left=�6B   $	��Top=�6]   $��0,52,0,0q�6u   �� 5E      -/   5        $ tb_idDenuncia��5E      $
��Left=�61   $��23��6L   $��	138,3,0,0q�6X   $
 Wrap=�6k   $	��Top=�6   $	��120��6�   �� 5F      -0   5        $ tb_tipoDenuncia��5F      $
��Left=�63   $��23��6N   $��
138,29,0,0q�6Z   $
 Wrap=�6n   $	��Top=�6�   $	��120��6�   �� 5G      -1   5        $ tb_fechaDenuncia��5G      $
��Left=�64   $��23��6O   $��
138,55,0,0q�6[   $
 Wrap=�6o   $	��Top=�6�   $	��120��6�   �� 5H      -2   5        $ tb_descripcionDenuncia��5H      $
��Left=�6:   $��55��6U   $��
138,81,0,0q�6a   $
 Wrap=�6u   $	��Top=�6�   $	��346��6�   �� 5I      -3   5        $ l_descripcionDenuncia��5I      $��DESCRIPCIÓN:��67   $
��Left=�6N   $��0,81,0,0q�6i   $	��Top=�6{   $��26��6�   $��86��6�   5J      5K      5M      5N      