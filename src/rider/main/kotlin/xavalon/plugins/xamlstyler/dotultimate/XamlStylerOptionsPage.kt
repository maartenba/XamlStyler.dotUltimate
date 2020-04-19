package xavalon.plugins.xamlstyler.dotultimate

import com.jetbrains.rider.settings.simple.SimpleOptionsPage

const val OPTIONS_PAGE_ID = "XamlStylerOptionsPage"

class XamlStylerOptionsPage : SimpleOptionsPage("XAML Styler", OPTIONS_PAGE_ID) {

    override fun getId(): String = OPTIONS_PAGE_ID
}